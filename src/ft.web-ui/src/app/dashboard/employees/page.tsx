import Link from 'next/link'

import { Button } from '@/components/ui/button'

import { columns } from './columns'
import { DataTable } from './data-table'
import { Employee } from '@/types/employee'
import PageTitle from '../components/PageTitle'

const employees: Employee[] = [
  {
    id: '1',
    firstName: 'John',
    middleName: 'A',
    lastName: 'Doe',
    salary: 7000,
    type: 'Full Time',
    email: 'john.doe@example.com'
  },
  {
    id: '2',
    firstName: 'Jane',
    middleName: 'B',
    lastName: 'Doe',
    salary: 8000,
    type: 'Part Time',
    email: 'jane.doe@example.com'
  },
  {
    id: '3',
    firstName: 'Mary',
    middleName: 'C',
    lastName: 'Johnson',
    salary: 7500,
    type: 'Contract',
    email: 'mary.johnson@example.com'
  },
  {
    id: '4',
    firstName: 'James',
    middleName: 'D',
    lastName: 'Smith',
    salary: 6000,
    type: 'Full Time',
    email: 'james.smith@example.com'
  },
  {
    id: '5',
    firstName: 'Patricia',
    middleName: 'E',
    lastName: 'Williams',
    salary: 7200,
    type: 'Part Time',
    email: 'patricia.williams@example.com'
  },
  {
    id: '6',
    firstName: 'Robert',
    middleName: 'F',
    lastName: 'Brown',
    salary: 7300,
    type: 'Contract',
    email: 'robert.brown@example.com'
  },
  {
    id: '7',
    firstName: 'Michael',
    middleName: 'G',
    lastName: 'Jones',
    salary: 7100,
    type: 'Full Time',
    email: 'michael.jones@example.com'
  },
  {
    id: '8',
    firstName: 'Linda',
    middleName: 'H',
    lastName: 'Miller',
    salary: 6800,
    type: 'Part Time',
    email: 'linda.miller@example.com'
  },
  {
    id: '9',
    firstName: 'William',
    middleName: 'I',
    lastName: 'Davis',
    salary: 6900,
    type: 'Contract',
    email: 'william.davis@example.com'
  },
  {
    id: '10',
    firstName: 'Elizabeth',
    middleName: 'J',
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
    <main className="flex w-full flex-col space-y-5">
      <PageTitle title="Employees" />
      <div className="flex items-center space-x-4">
        <input
          className="h-10 w-96 rounded-md border pl-4"
          placeholder="Search Employees..."
          type="text"
        />
      </div>
      <div className="flex flex-col space-y-10">
        <DataTable columns={columns} data={data}></DataTable>
          <Link href="/dashboard/employees/new">
            <Button className="ml-auto w-72">Add Employee</Button>
          </Link>
        </div>
    </main>
  )
}
