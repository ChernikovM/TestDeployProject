const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:31340';

const PROXY_CONFIG = [
  {
    "/api/*": {
      "target": target,
      "secure": false,
      "pathRewrite": {"^/api" : ""}
    }
  }
]

module.exports = PROXY_CONFIG;
