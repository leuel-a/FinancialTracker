import dotenv from 'dotenv'
import app from './src/index'
import logger from './src/utils/logger'
import * as db from './src/config/db'

dotenv.config()
const PORT = process.env.PORT || 3000

app.listen(PORT, async () => {
  logger.info(`Server is running on port ${PORT}`)

  await db.connect()
})