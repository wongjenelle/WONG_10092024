const { env } = require('process');

// const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//     env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7276';

const target = 'https://localhost:7276';


const PROXY_CONFIG = [
  {
    context: [
      "/videos",
    ],
    target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
