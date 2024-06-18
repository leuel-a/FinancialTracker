import { Skeleton } from '@/components/ui/skeleton'

export function TableSkeleton() {
  return (<div className="flex flex-col gap-3 items-center justify-center">
    {Array.from({ length: 10 }).map((_, i) => (
      <Skeleton className="h-12 w-full" />
    ))}
    <div className="flex items-center justify-end gap-2">
      <Skeleton className="w-12" />
    </div>
  </div>)
}