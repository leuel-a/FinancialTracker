'use client'

import Link from 'next/link'
import { LucideIcon } from 'lucide-react'

import { cn } from '@/lib/utils'
import { buttonVariants } from '@/components/ui/button'

import { Tooltip, TooltipContent, TooltipTrigger, TooltipProvider } from '@/components/ui/tooltip'

interface NavProps {
  selected: string
  setSelected: (selected: string) => void
  isCollapsed: boolean
  links: {
    title: string
    label?: string
    icon: LucideIcon
    variant: 'default' | 'ghost'
    href: string
  }[]
}

export function getBestMatch(
  pathname: string,
  links: {
    title: string
    label?: string
    variant: 'default' | 'ghost'
    href: string
  }[]
) {
  let bestMatch = ''
  let maxLen = 0

  links.forEach(link => {
    const href = link.href
    if (
      pathname.startsWith(href) &&
      (pathname.length === href.length || pathname[href.length] === '/')
    ) {
      if (href.length > maxLen) {
        bestMatch = href
        maxLen = href.length
      }
    }
  })

  return bestMatch // Returns the href that is the longest match
}

export function Nav({ links, isCollapsed, setSelected, selected }: NavProps) {
  return (
    <TooltipProvider>
      <div
        data-collapsed={isCollapsed}
        className="group flex flex-col gap-4 py-2 data-[collapsed=true]:py-2"
      >
        <nav
          className="grid gap-1 px-2 group-[[data-collapsed=true]]:justify-center group-[[data-collapsed=true]]:px-2">
          {links.map((link, index) =>
            isCollapsed ? (
              <Tooltip key={index} delayDuration={0}>
                <TooltipTrigger asChild>
                  <Link
                    href={link.href}
                    onClick={() => setSelected(link.href)}
                    className={cn(
                      buttonVariants({
                        variant: link.href === selected ? 'default' : 'ghost',
                        size: 'icon'
                      }),
                      'h-9 w-9',
                      link.variant === 'default' &&
                      'dark:bg-muted dark:text-muted-foreground dark:hover:bg-muted dark:hover:text-white'
                    )}
                  >
                    <link.icon className="h-4 w-4" />
                    <span className="sr-only">{link.title}</span>
                  </Link>
                </TooltipTrigger>
                <TooltipContent side="right" className="flex items-center gap-4">
                  {link.title}
                  {link.label && (
                    <span className="ml-auto text-muted-foreground">{link.label}</span>
                  )}
                </TooltipContent>
              </Tooltip>
            ) : (
              <Link
                key={index}
                onClick={() => setSelected(link.href)}
                href={link.href}
                className={cn(
                  buttonVariants({
                    variant: link.href == selected ? 'default' : 'ghost',
                    size: 'sm'
                  }),
                  link.variant === 'default' &&
                  'dark:bg-muted dark:text-white dark:hover:bg-muted dark:hover:text-white',
                  'justify-start',
                )}
              >
                <link.icon className="mr-2 h-4 w-4" />
                {link.title}
                {link.label && (
                  <span
                    className={cn(
                      'ml-auto',
                      link.variant === 'default' && 'text-background dark:text-white'
                    )}
                  >
                    {link.label}
                  </span>
                )}
              </Link>
            )
          )}
        </nav>
      </div>
    </TooltipProvider>
  )
}
