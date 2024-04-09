import Link from 'next/link'

export default function SideBarLink() {
  return (
    <div className="flex items-center space-x-2">
      <Link className="text-gray-600" href="/">
        Dashboard
      </Link>
    </div>
  )
}
