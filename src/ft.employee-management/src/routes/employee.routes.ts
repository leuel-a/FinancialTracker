import * as db from '../config/db'
import { Router, Request, Response } from 'express'
import requireUser from '../middlewares/requireUser'
import validate from '../middlewares/validateResource'
import { createEmployeeSchema } from '../schemas/employee.schema'
import { createEmployeeHandler } from '../controllers/employee.controller'

const router = Router()

router.get('/status', (req: Request, res: Response) => {
  return res.json({ mongoose: db.isAlive() })
})

router.post('/', validate(createEmployeeSchema), requireUser, createEmployeeHandler)

export default router
