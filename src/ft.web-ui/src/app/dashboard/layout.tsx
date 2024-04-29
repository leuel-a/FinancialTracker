import React from 'react'
import Navbar from '../../components/Navbar'

interface Props {
  children: React.ReactNode
}

export default function DashboardLayout({ children }: Props) {
  return (
    <div className="flex">
      <Navbar />
      {children}
    </div>
  )
}
