import Link from 'next/link'
import { Undo2 } from 'lucide-react'
import PageTitle from '../../components/PageTitle'

export default function AddTransaction() {
  return (
    <div>
      <div className="flex justify-between">
        <PageTitle title="Add Transaction" />
        <Link
          href="/dashboard/transactions"
          className="flex cursor-pointer items-center gap-2 rounded-lg border border-black px-4 py-2 hover:border hover:shadow-md"
        >
          <Undo2 />
          <p>Back to Transactions</p>
        </Link>
      </div>
    </div>
  )
}
