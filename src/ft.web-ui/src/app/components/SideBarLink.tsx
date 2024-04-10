import { FC } from 'react'

import Link from 'next/link'
import { DashboardLink } from '@/app/types/dashboard'

interface SideBarLinkProps {
  data: DashboardLink
}

const SideBarLink: FC<SideBarLinkProps> = ({ data: { route, name, Icon } }) => {
  return (
    <div className="flex items-center space-x-2">
      <Icon className="inline-block text-gray-600" />
      <Link className="text-gray-600" href={`/dashboard/${route}`}>
        {name}
      </Link>
    </div>
  )
}

export default SideBarLink
