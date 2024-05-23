'use client'

import Link from 'next/link'
import { cn } from '@/lib/utils'
import { useState } from 'react'
import { usePathname } from 'next/navigation'

const links: { title: string; description: string; href: string }[] = [
  {
    title: 'User Management',
    description: '',
    href: '/dashboard/admin/settings/users'
  },
  {
    title: 'Role Management',
    description: '',
    href: '/dashboard/admin/settings/roles'
  }
]

export default function SettingsSideNavBar({
  className,
  ...props
}: React.HtmlHTMLAttributes<HTMLDivElement>) {
  const pathname = usePathname()
  return (
    <div
      {...props}
      className={cn(className, 'mr-6 flex flex-col items-center justify-start gap-2 pt-2')}
    >
      {links.map(({ href, title }, index) => (
        <div
          className={cn(
            'w-full rounded-lg px-2 py-4 outline outline-[1px] outline-offset-2 hover:bg-zinc-700 hover:text-white',
            pathname === href && "bg-zinc-700 text-white"
          )}
        >
          <Link className="cursor-pointer" href={href}>
            {title}
          </Link>
        </div>
      ))}
    </div>
  )
}
