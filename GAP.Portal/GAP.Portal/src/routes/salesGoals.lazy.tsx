import { createLazyFileRoute } from '@tanstack/react-router'
import SalesGoals from '../components/SalesGoals'

export const Route = createLazyFileRoute('/salesGoals')({
  component: SalesGoals
})