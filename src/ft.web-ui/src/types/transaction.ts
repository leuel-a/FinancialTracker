import { Category } from './categories'

export type Transaction = {
  id: number
  date: Date
  type: string
  amount: number
  status: string
  region?: string | null
  description?: string | null
  paymentMethod?: string | null
  quantity?: number
  unitPrice?: number
  accountNumber?: string
  categoryId: number | null
  category: Category | null
}
