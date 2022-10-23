import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Questions',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44395/',
    redirectUri: baseUrl,
    clientId: 'Questions_App',
    responseType: 'code',
    scope: 'offline_access Questions',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44395',
      rootNamespace: 'MaXiaoMing.Questions',
    },
  },
} as Environment;
