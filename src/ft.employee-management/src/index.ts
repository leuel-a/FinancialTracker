import cors from 'cors'
import helmet from 'helmet'
import * as dotenv from 'dotenv'
import express, { Express, Request, Response } from 'express'
import employeeRoutes from './routes/employee.routes'

dotenv.config()

const app: Express = express()

app.use(helmet())
app.use(cors())
app.use(express.json())

app.use('/api/employees', employeeRoutes)

app.get('/status', (req: Request, res: Response) => {
  res.status(200).json({ status: 'OK' })
})

export default app