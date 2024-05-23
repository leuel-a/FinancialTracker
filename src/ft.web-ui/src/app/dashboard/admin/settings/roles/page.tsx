import { Button } from '@/components/ui/button'
import React from 'react'

export default function RolesPage() {
  return (
    <div className="pl-5">
      <div className='flex justify-between'>
        <h2 className="text-lg font-semibold">Role Management</h2>
        <Button className='w-40 bg-zinc-700 font-semibold'>Create Role</Button>
      </div>
    </div>
  )
}
