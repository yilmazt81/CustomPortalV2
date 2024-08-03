import React from 'react' 


const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))
const BranchDefination = React.lazy(() => import('./views/branchdefination/'))
const AdressDefination = React.lazy(() => import('./views/adressdefination'))
const FormDefinationType = React.lazy(() => import('./views/formdefinations'))
const ProductDefination = React.lazy(() => import('./views/productdefination')) 
const UserDefination = React.lazy(() => import('./views/userdefination'))
const FormDefinationEdit=  React.lazy(() => import('./views/formdefinationEdit'))
const DigitalForms=React.lazy(() => import('./views/digitalForms'))
const DigitalFormEdit=React.lazy(() => import('./views/digitalFormEdit'))

const routes = [

  
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', element: Dashboard }, 
  { path: '/BranchDefination', name: 'BranchDefination', element: BranchDefination },   
  { path: '/AdressDefination', name: 'AdressDefination', element: AdressDefination }, 
  { path: '/FormDefinationType', name: 'FormDefinationType', element: FormDefinationType }, 
  { path: '/productdefination', name: 'productdefination', element: ProductDefination }, 
  { path: '/userdefination', name: 'Userdefination', element: UserDefination }, 
  { path: '/FormDefinationTypeEdit', name: 'FormDefinationEdit', element: FormDefinationEdit }, 
  
  { path: '/digitalForms', name: 'DigitalForms', element: DigitalForms }, 
  { path: '/digitalFormEdit', name: 'DigitalForms/digitalFormEdit', element: DigitalFormEdit }, 
  
]
export default routes
