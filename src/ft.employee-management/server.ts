import dotenv from 'dotenv'
import app from './src/index'

dotenv.config()
const PORT = process.env.PORT || 3000

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`)
})