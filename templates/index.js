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

const skipList = new Set([
  'FactorDevice.links',
  'Link.hints',
  'User._links',
  'UserGroup._embedded',
  'UserGroup._links',
  'UserGroupStats._links'
]);

const renameList = {
  'ActivationToken.activationToken': 'token'
};

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
    case 'array': return `IList<${getType(prop.model)}>`;
    case 'object': return prop.model;
    case 'hash': return `IDictionary<string, ${getType(prop.model)}>`;
    default: return getType(prop.commonType);
  }
}

function getterName(prop) {
  if (prop.commonType === 'array') {
    return `GetArrayProperty<${getType(prop.model)}>`;
  }

  const clrType = propToCLRType(prop);

  switch (clrType) {
    case 'bool?': return 'GetBooleanProperty';
    case 'int?': return 'GetIntegerProperty';
    case 'DateTimeOffset?': return 'GetDateTimeProperty';
    case 'string': return 'GetStringProperty';
    default: return `GetResourceProperty<${clrType}>`;
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
      let fullPath = `${model.modelName}.${property.propertyName}`;

      if (property.model && property.model === 'object') {
        console.log('Skipping object property', fullPath);
        property.hidden = true;
        continue;
      }

      if (typeof property.commonType === 'undefined') {
        console.log('Skipping property without commonType', fullPath);
        property.hidden = true;
        continue;
      }
      
      if (skipList.has(fullPath)) {
        console.log('Skipping property on skipList', fullPath);
        property.hidden = true;
        continue;
      }

      if (renameList.hasOwnProperty(fullPath)) {
        let newName = renameList[fullPath];
        console.log(`Renaming property ${fullPath} to ${newName}`);
        property.displayName = newName;
      }

      // skip
      // User._links
      // UserGroup._embedded, UserGroup._links
    }

    templates.push({
      src: 'Model.cs.hbs',
      dest: `Generated/${model.modelName}.Generated.cs`,
      context: model
    });
  }
  
  templates.push({
    src: 'IOktaClient.cs.hbs',
    dest: `Generated/IOktaClient.Generated.cs`,
    context: {
      spec,
      operations
    }
  });

  templates.push({
    src: 'OktaClient.cs.hbs',
    dest: `Generated/OktaClient.Generated.cs`,
    context: {
      spec,
      operations
    }
  });

  return templates;
}
