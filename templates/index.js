const csharp = module.exports;

/**
 * This file is used by the @okta/openapi generator.  It defines language-specific
 * post-processing of the JSON spec, as well as handebars helpers.  This file is meant
 * to give you control over the data that handlebars uses when processing your templates
 */

const partialUpdateList = new Set([
  'User',
  'UserProfile'
]);

const getType = (specType) => {
  switch(specType) {
    case 'boolean': return 'bool?';
    case 'integer': return 'int?';
    case 'dateTime': return 'DateTimeOffset?';
    default: return specType;
  }
};

function paramToCLRType(param) {
  return getType(param.type);
}

function nbsp(times) {
  if (typeof times !== 'number')times = 1;
  return ' '.repeat(times);
}

function propToCLRType(prop) {
  switch (prop.commonType) {
    case 'array': return `${getType(prop.model)}[]`;
    case 'object': return prop.model;
    case 'hash': return `IDictionary<string, ${getType(prop.model)}>`;
    default: return getType(prop.commonType);
  }
}

function getterName(prop) {
  const clrType = propToCLRType(prop);
  switch (clrType) {
    case 'bool?': return 'GetBooleanProperty';
    case 'int?': return 'GetIntegerProperty';
    case 'DateTimeOffset?': return 'GetDateTimeProperty';
    case 'string': return 'GetStringProperty';
    default: return `GetProperty<${clrType}>`;
  }
}

function exists(obj, key) {
  return obj && obj.hasOwnProperty(key);
}

csharp.process = ({spec, operations, models, handlebars}) => {

  handlebars.registerHelper({
    paramToCLRType,
    propToCLRType,
    getterName,
    exists,
    nbsp
  });

  const templates = [];

  // add all the models
  for (let model of models) {
    model.specVersion = spec.info.version;

    if (partialUpdateList.has(model.modelName)) {
      model.supportsPartialUpdates = true;
    }

    for (let property of model.properties) {
      if (property.model === 'object') {
        property.hidden = true;
        continue;
      }
    }

    templates.push({
      src: 'Model.cs.hbs',
      dest: `Models/${model.modelName}.Generated.cs`,
      context: model
    });
  }
  
  templates.push({
    src: 'IOktaClient.cs.hbs',
    dest: `IOktaClient.Generated.cs`,
    context: {
      spec,
      operations
    }
  });

  templates.push({
    src: 'OktaClient.cs.hbs',
    dest: `OktaClient.Generated.cs`,
    context: {
      spec,
      operations
    }
  });

  return templates;
}
