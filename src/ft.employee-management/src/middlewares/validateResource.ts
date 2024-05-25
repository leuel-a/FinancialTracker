import logger from '../utils/logger'
import { AnyZodObject } from 'zod'
import { NextFunction, Request, Response } from 'express'

const validate = (schema: AnyZodObject) => async (req: Request, res: Response, next: NextFunction) => {
  try {
    schema.parse({
      body: req.body,
      query: req.query,
      params: req.params
    })
  } catch (e) {
    logger.error(e)
    return res.status(400).json({ error: e.errors })
  }
}

export default validate