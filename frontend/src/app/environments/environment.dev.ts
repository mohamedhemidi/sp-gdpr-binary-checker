const API_BASE_URL = {
  dev: 'http://localhost:5018',
  test: '',
  uat: '',
  production: '',
};

const ENV = 'dev';

const API_ENDPOINT = API_BASE_URL[ENV];

export const env = {
  API_ENDPOINT,
  //Authenticaion:
  Login: `${API_ENDPOINT}/api/auth/login`,
  Signup: `${API_ENDPOINT}/api/auth/register`,

  // Check Tool:
  CheckEntry: `${API_ENDPOINT}/api/entries/check`,
  GetAllEntries: `${API_ENDPOINT}/api/entries`,
};
