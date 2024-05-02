import { Link as DashboardLink } from '@/types/link'
import Link from 'next/link'

interface Props {
  link: DashboardLink
}

export default function NavLink({ link: { name, Icon } }: Props) {
  const route = name === 'Dashboard' ? '/dashboard' : `/dashboard/${name.toLowerCase()}`

  return (
    <Link href={route}>
      <div className="flex items-center gap-2 rounded-md py-3 pl-3 hover:bg-gray-200 hover:text-zinc-700">
        <Icon className="text-lg" />
        <h3 className="text-md">{name}</h3>
      </div>
    </Link>
  )
}
