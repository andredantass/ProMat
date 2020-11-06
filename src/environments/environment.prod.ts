export const environment = {
  timeoutSessionIdle: 900,
  apiURL: 'https://promatapi.azurewebsites.net/api',
  appInsights: {
      instrumentationKey: '1ffaed42-6265-458a-ae0d-ffd0600ac948'
  },
  SSO_BASE_URL: 'https://ssohmo.blueopex.com',
  auth: {
      ROOT_URL: "https://localhost:5000/app/auth",
      AUTH_URL: 'https://ssohmo.blueopex.com/',
      AUTH_CLINTID: 'bolight.azurewebsites.net',
      REQUERID_SCOPE: 'openid profile offline_access',
      LOGOUT_URL: 'https://ssohmo.blueopex.com/Account/logout',

      RESPONSE_TYPE: 'token id_token',
      REQUEST_CLAIMS: ['company']
  }, 
  production: true
};