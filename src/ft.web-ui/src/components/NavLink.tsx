import { Link as DashboardLink } from '@/types/link'
import Link from 'next/link'

interface Props {
  link: DashboardLink
}

export default function NavLink({ link: { name, Icon } }: Props) {
  const route = name === 'Dashboard' ? '/dashboard' : `/dashboard/${name.toLowerCase()}`

  console.log(route)

  return (
    <Link href={route}>
      <div className="flex items-center gap-2 rounded-md py-3 pl-3 hover:bg-gray-200 hover:text-purple-600">
        <Icon className="text-xl" />
        <h3 className="text-lg">{name}</h3>
      </div>
    </Link>
  )
}
