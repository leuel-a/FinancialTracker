import _ from 'lodash'
import { verifyJwt } from '../utils/jwt.utils'
import { NextFunction, Request, Response } from 'express'

const deserializeUser = async (request: Request, response: Response, next: NextFunction) => {
  const accessToken = _.get(request, 'headers.authorization', '').replace(/^Bearer\s/, '')
  const refreshToken = _.get(request, 'headers.x-refresh-token')

  if (!accessToken) {
    return next()
  }

  const { decoded, expired } = verifyJwt(accessToken)
  
  if (decoded !== nul) {
    request.locals.user = decoded
    return next()
  }
  
  if (expired && refreshToken) {
    // Get access token from the user management service
    
  }
}