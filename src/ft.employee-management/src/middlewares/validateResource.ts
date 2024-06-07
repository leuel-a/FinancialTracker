import logger from '../utils/logger'
import { AnyZodObject } from 'zod'
import { NextFunction, Request, Response } from 'express'

const validate = (schema: AnyZodObject) => async (req:Request, res:    Response, next: NextFunction) => {
  try {
    schema.parse({
      body: req.body,
      query: req.query,
      params: req.params
    })
    return next();
  } catch (e: any) {
    logger.error(e)
    return res.json({errors: e})
  }
}

export default validate