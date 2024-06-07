import _ from 'lodash'
import logger from '../utils/logger'
import axios, { AxiosResponse } from 'axios'
import { verifyJwt } from '../utils/jwt.utils'
import { NextFunction, Request, Response } from 'express'

interface RefreshTokenRequestResponse {
  message: string
  data: {
    accessToken: string
  },
  errors?: string[]
}

const deserializeUser = async (request: Request, response: Response, next: NextFunction) => {
  const accessToken = _.get(request, 'headers.authorization', '').replace(/^Bearer\s/, '')
  const refreshToken = _.get(request, 'headers.x-refresh')


  if (!accessToken) {
    return next()
  }

  const { decoded, expired } = verifyJwt(accessToken)

  if (decoded !== null) {
    response.locals.user = decoded
    return next()
  }
  
  if (expired && refreshToken) {
    // Get access token from the user management service
    const {
      data: {
        data, errors, message
      }
    }: AxiosResponse<RefreshTokenRequestResponse> = await axios.post<RefreshTokenRequestResponse>(
      'http://localhost:5000/api/auth/refresh', {
        accessToken,
        refreshToken
      })

    if (errors) {
      logger.error(`Message: ${message}; Errors: ${errors.join(' ')}`)
      return next()
    }

    const newAccessToken = data.accessToken
    response.setHeader('x-access-token', newAccessToken)

    const { decoded } = verifyJwt(newAccessToken)
    response.locals.user = decoded
    return next()
  }

  return next()
}

export default deserializeUser