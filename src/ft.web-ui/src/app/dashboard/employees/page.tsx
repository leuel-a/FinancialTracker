'use client'

import Link from 'next/link'
import { Button } from '@/components/ui/button'
import { useSearchParams } from 'next/navigation'

import { columns } from './columns'
import { DataTable } from './data-table'
import PageTitle from '../components/PageTitle'
import { useGetAllEmployeesQuery } from '@/features/employees/employeesApi'
import { TableSkeleton } from '@/app/components/TableSkeleton'

export default function Page() {
  const searchParams = useSearchParams()
  const { isLoading, isError, data } = useGetAllEmployeesQuery(searchParams.toString())

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
      <div className="flex flex-col space-y-10 relative">
        {isLoading ? <TableSkeleton /> : <DataTable columns={columns} previousPage={data?.previousPage || undefined}
                                                    nextPage={data?.nextPage || undefined}
                                                    data={data?.data || []}></DataTable>}
        <Link href="/dashboard/employees/new" className="absolute bottom-5">
            <Button className="ml-auto w-72">Add Employee</Button>
          </Link>
        </div>
    </main>
  )
}
