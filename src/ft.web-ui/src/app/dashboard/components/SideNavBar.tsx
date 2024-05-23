'use client'

import { Nav } from './ui/nav'
import { checkAdmin } from '@/lib/auth.client'
import { useState, useEffect } from 'react'
import { Button } from '@/components/ui/button'
import { ChevronRight, ChevronLeft } from 'lucide-react'
import { useWindowWidth } from '@react-hook/window-size'
import { adminTopLinks, adminBottomLinks, topLinks, bottomLinks } from '../data'

export default function SideNavBar() {
  const [selected, setSelected] = useState('/dashboard')
  const [isAdmin, setIsAdmin] = useState<boolean>(false)
  const [isCollapsed, setIsCollapsed] = useState<boolean>(false)

  useEffect(() => {
    checkAdmin(setIsAdmin)
  }, [])

  const onlyWidth = useWindowWidth()
  const mobile = onlyWidth < 768

  return (
    <div className="relative flex min-w-20 flex-col justify-between border-r px-3 pb-10 pt-24 transition-all">
      {!mobile && (
        <div className="absolute -right-6 top-7">
          <Button
            onClick={() => {
              setIsCollapsed(prev => !prev)
            }}
            variant="secondary"
            className="rounded-full border border-black shadow-lg"
          >
            {!isCollapsed ? <ChevronLeft size={14} color="black" /> : <ChevronRight size={14} />}
          </Button>
        </div>
      )}
      <Nav
        links={isAdmin ? adminTopLinks : topLinks}
        isCollapsed={mobile ? true : isCollapsed}
        selected={selected}
        setSelected={setSelected}
      />
      <Nav
        isCollapsed={mobile ? true : isCollapsed}
        selected={selected}
        setSelected={setSelected}
        links={isAdmin ? adminBottomLinks : bottomLinks}
      />
    </div>
  )
}
