import { Category } from './categories'

export const months = ['January', 'February', 'March', 'April', 'May', 'June',
  'July', 'August', 'September', 'October', 'November', 'December']

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

export type MonthlyTransactionSummary = {
  month: number | string
  income: number
  expense: number
  total: number
}