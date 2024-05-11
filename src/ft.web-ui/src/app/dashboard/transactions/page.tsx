import PageTitle from '../components/PageTitle'
import { DataTable, columns } from './data-table'

import { transactions } from './data'

export default function AnalysisPage() {
  return (
    <div className="flex flex-col gap-5">
      <PageTitle title="Transactions" />
      <DataTable columns={columns} data={[...transactions, ...transactions, ...transactions]} />
    </div>
  )
}
