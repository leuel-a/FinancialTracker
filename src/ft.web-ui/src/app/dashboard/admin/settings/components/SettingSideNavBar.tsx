'use client'

import Link from 'next/link'
import { cn } from '@/lib/utils'
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
      className={cn(className, 'mr-6 flex flex-col items-start justify-start gap-2 pt-2')}
    >
      {links.map(({ href, title }, index) => (
        <>
          <Link
            className={cn('w-full cursor-pointer rounded-md px-4 py-2 hover:bg-gray-100', pathname === href && 'bg-gray-100 border-[1px] shadow-md')}
            href={href}
          >
            {title}
          </Link>
          {index !== links.length - 1 && <div className="h-px w-full bg-gray-200" />}
        </>
      ))}
    </div>
  )
}
