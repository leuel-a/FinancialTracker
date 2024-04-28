import { Link } from '@/types/link'

interface Props {
  link: Link
}

export default function NavLink({ link: { name, Icon } }: Props) {
  return (
    <div className="flex items-center gap-2 rounded-md py-3 pl-3 hover:bg-gray-200 hover:text-purple-600">
      <Icon className="text-xl" />
      <h3 className="text-lg">{name}</h3>
    </div>
  )
}
