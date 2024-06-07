// "Issuer": "http://localhost:5000",
//   "Audience": "http://localhost:5000",
//   "AccessTokenKey": "91b18586f916e8434f36dc778f7e482dda603e72a543fdd28f95172b40d350df",
//   "RefreshTokenKey": "91b18586f916e8434f36dc778f7e482dda603e72a543fdd28f95172b40d350df",
//   "AccessTokenTTL": 15,
//   "RefreshTokenTTL": 60

const accessTokenKey = '91b18586f916e8434f36dc778f7e482dda603e72a543fdd28f95172b40d350df'
const refreshTokenKey = '91b18586f916e8434f36dc778f7e482dda603e72a543fdd28f95172b40d350df'

export default {
  port: 5003,
  dbUri: 'mongodb://localhost:27017/FTEmployeeManagementDB',
  accessTokenKey,
  refreshTokenKey,
  accessTokenTTL: '15m',
  refreshTokenTTL: '60m',
  issuer: 'http://localhost:5000',
  audience: 'http://localhost:5000'
}