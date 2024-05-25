import { Router } from 'express'
import validate from '../middlewares/validateResource'
import { createEmployeeSchema } from '../schemas/employee.schema'

const router = Router()

router.post('/', validate(createEmployeeSchema), (req, res) => {
  res.json({ message: 'Create a new employee' })
})

export default router