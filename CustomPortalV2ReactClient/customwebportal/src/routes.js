import React from 'react' 


const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const BranchDefination = React.lazy(() => import('./views/branchdefination/'))
const AdressDefination = React.lazy(() => import('./views/adressdefination'))
const FormDefinationType = React.lazy(() => import('./views/formdefinations'))
const ProductDefination = React.lazy(() => import('./views/productdefination'))
 
const UserDefination = React.lazy(() => import('./views/userdefination'))

const routes = [

  
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', element: Dashboard }, 
  { path: '/BranchDefination', name: 'BranchDefination', element: BranchDefination },   
  { path: '/AdressDefination', name: 'AdressDefination', element: AdressDefination }, 
  { path: '/FormDefinationType', name: 'FormDefinationType', element: FormDefinationType }, 
  { path: '/productdefination', name: 'productdefination', element: ProductDefination }, 
  { path: '/userdefination', name: 'userdefination', element: UserDefination }, 
  
]
export default routes
