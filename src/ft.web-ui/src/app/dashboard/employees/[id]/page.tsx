import {format} from 'date-fns'
import { Employee } from '@/types/employee'
import PageTitle from '@/app/dashboard/components/PageTitle'
import { Label } from '@/components/ui/label'

export default async function({ params: { id } }: { params: { id: number } }) {
  const response = await fetch(`http://localhost:5000/api/employees/${id}`)
  const data: Employee = await response.json()
  return <div className="space-y-8">
    <div className="flex flex-col gap-6">
      <h1 className="text-3xl underline font-semibold">{data.firstName} {data.lastName}</h1>
      <div className="border-2 border-gray-500 rounded-md px-3 py-5 space-y-4">
        <h2 className="text-xl font-semibold">Personal Information</h2>
        <div className="grid grid-cols-12 justify-start items-center">
          <Label className="col-span-4 font-semibold">First Name</Label>
          <p>{data.firstName}</p>
        </div>
        <div className="grid grid-cols-12 justify-start items-center">
          <Label className="col-span-4 font-semibold">Last Name</Label>
          <p>{data.lastName}</p>
        </div>
        <div className="grid grid-cols-12 justify-start items-center">
          <Label className="col-span-4 font-semibold">Date Of Birth</Label>
          <p>{data.dateOfBirth ? format(data.dateOfBirth, "yyyy/MM/dd") : 'Unspecified'}</p>
        </div>
      </div>  
    </div>
  </div>
}