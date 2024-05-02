import Link from 'next/link'

import { Button } from '@/components/ui/button'

import { columns } from './columns'
import { DataTable } from './data-table'
import Employee from '@/types/employee'

const employees: Employee[] = [
  {
    id: '1',
    firstName: 'John',
    lastName: 'Doe',
    salary: 7000,
    type: 'Full Time',
    email: 'john.doe@example.com'
  },
  {
    id: '2',
    firstName: 'Jane',
    lastName: 'Doe',
    salary: 8000,
    type: 'Part Time',
    email: 'jane.doe@example.com'
  },
  {
    id: '3',
    firstName: 'Mary',
    lastName: 'Johnson',
    salary: 7500,
    type: 'Contract',
    email: 'mary.johnson@example.com'
  },
  {
    id: '4',
    firstName: 'James',
    lastName: 'Smith',
    salary: 6000,
    type: 'Full Time',
    email: 'james.smith@example.com'
  },
  {
    id: '5',
    firstName: 'Patricia',
    lastName: 'Williams',
    salary: 7200,
    type: 'Part Time',
    email: 'patricia.williams@example.com'
  },
  {
    id: '6',
    firstName: 'Robert',
    lastName: 'Brown',
    salary: 7300,
    type: 'Contract',
    email: 'robert.brown@example.com'
  },
  {
    id: '7',
    firstName: 'Michael',
    lastName: 'Jones',
    salary: 7100,
    type: 'Full Time',
    email: 'michael.jones@example.com'
  },
  {
    id: '8',
    firstName: 'Linda',
    lastName: 'Miller',
    salary: 6800,
    type: 'Part Time',
    email: 'linda.miller@example.com'
  },
  {
    id: '9',
    firstName: 'William',
    lastName: 'Davis',
    salary: 6900,
    type: 'Contract',
    email: 'william.davis@example.com'
  },
  {
    id: '10',
    firstName: 'Elizabeth',
    lastName: 'Garcia',
    salary: 6500,
    type: 'Full Time',
    email: 'elizabeth.garcia@example.com'
  }
]

// const employeeTypes = Array.from(new Set(employees.map(employee => employee.type)))

export default function Page() {
  const data = employees

  return (
    <main className="mt-10 flex w-full flex-col space-y-5 px-10">
      <div>
        <div>
          <h1 className="text-2xl font-semibold">Employees</h1>
        </div>
      </div>
      <div className="flex items-center space-x-4">
        <input
          className="h-10 w-96 rounded-md border pl-4"
          placeholder="Search Employees..."
          type="text"
        />
      </div>
      <div className="flex flex-col space-y-10">
        <DataTable columns={columns} data={data}></DataTable>
        {/* TODO: come up with a better solution than just wrapping the Link with a div */}
        <div>
          <Link href="/dashboard/employees/new">
            <Button className="ml-auto w-72">Add Employee</Button>
          </Link>
        </div>
      </div>
    </main>
  )
}
