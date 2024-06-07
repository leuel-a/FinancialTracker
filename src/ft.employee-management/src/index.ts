import cors from 'cors'
import helmet from 'helmet'
import * as dotenv from 'dotenv'
import employeeRoutes from './routes/employee.routes'
import express, { Express, Request, Response } from 'express'
import deserializeUser from './middlewares/deserializeUser'

dotenv.config()

const app: Express = express()

app.use(helmet())
app.use(cors())
app.use(express.json())

app.use(deserializeUser)
app.use('/api/employees', employeeRoutes)

export default app