import NewEmployeeForm from '@/components/NewEmployeeForm'
import { IoMdArrowBack } from 'react-icons/io'

export default function Page() {
  return (
    <div className="mt-8 w-full">
      <div className="flex items-center gap-10">
        <div className="rounded-full bg-zinc-600 p-1 hover:bg-zinc-700">
          <IoMdArrowBack className="my-auto block text-xl text-white" />
        </div>
        <h1 className="text-2xl font-semibold text-zinc-700">Add Employee</h1>
      </div>
      <div className='mt-10 px-10 m-auto'>
        <NewEmployeeForm />
      </div>
    </div>
  )
}
