import NavLink from './NavLink'
import { links, bottomLinks } from '@/data/dasboardLinks'

export default function Navbar() {
  return (
    <div className="flex h-screen w-80 flex-col justify-between bg-white py-8 p-5">
      <div className="flex flex-col space-y-3">
        {links.map(link => {
          return <NavLink key={link.name} link={link} />
        })}
      </div>
      <div>
        {bottomLinks.map(link => {
          return <NavLink key={link.name} link={link} />
        })}
      </div>
    </div>
  )
}
