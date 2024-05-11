'use client'

import { ReactElement, useState } from 'react'

export function useMultiStepForm(
  steps: ((props: {
    isFirstStep: any
    isLastStep: any
    onNext: any
    onPrevious: any
  }) => ReactElement)[]
) {
  const [currentStepIndex, setCurrentStepIndex] = useState<number>(0)

  const navigationProps = {
    onNext: () => setCurrentStepIndex(i => (i < steps.length - 1 ? i + 1 : i)),
    onPrevious: () => setCurrentStepIndex(i => (i > 0 ? i - 1 : i)),
    goToStep: (index: number) => setCurrentStepIndex(index)
  }
  const isFirstStep = currentStepIndex === 0
  const isLastStep = currentStepIndex === steps.length - 1

  const stepComponents = steps.map((step, index) =>
    step({
      ...navigationProps,
      isFirstStep: index == 0,
      isLastStep: index == steps.length - 1
    })
  )

  const currentStep = steps[currentStepIndex]({
    ...navigationProps,
    isFirstStep,
    isLastStep
  })

  return {
    steps: stepComponents,
    currentStep,
    currentStepIndex,
    isFirstStep,
    isLastStep,
    ...navigationProps
  }
}
