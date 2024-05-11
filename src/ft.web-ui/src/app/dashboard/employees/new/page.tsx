import PageTitle from '../../components/PageTitle'
import { Undo2 } from 'lucide-react'
import Link from 'next/link'
import NewEmployeeForm from './components/NewEmployeesForm'

export default function Page() {
  return (
    <div className="flex w-full flex-col gap-5">
      <div className="flex justify-between">
        <PageTitle title="Add Employee" />
        <Link
          href="/dashboard/employees"
          className="flex cursor-pointer items-center gap-2 rounded-lg border border-black px-4 py-2 hover:border hover:shadow-md"
        >
          <Undo2 />
          <p>Back to Employees</p>
        </Link>
      </div>
      <NewEmployeeForm />
    </div>
  )
}
