import { CiSearch } from 'react-icons/ci'

export default function Page() {
  return (
    <main className="mt-5 flex flex-col space-y-5">
      <div>
        <div>
          <h1 className="text-2xl font-semibold">Employees</h1>
        </div>
        <div></div>
      </div>
      <div className='flex items-center space-x-4'>
        <input
          className="h-10 w-96 rounded-md border pl-4"
          placeholder="Search Employees..."
          type="text"
        />
        <CiSearch className='text-3xl'/>
      </div>
      <div></div>
    </main>
  )
}
