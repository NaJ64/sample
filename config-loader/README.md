# `config-loader`

> Exposes a simple convention-based abstraction for loading configuration.
> Default `JsonConfigurationLoader` defaults to loading from file `appsettings.{NODE_ENV}.json` in the current working directory

## Usage

```
const { JsonConfigurationLoader } = require('config-loader');

const config = new JsonConfigurationLoader().load(); // returns Map<string, any>

const hostName = config.get("hostName");
```
