import React from 'react' 


const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard.jsx'))
const BranchDefination = React.lazy(() => import('./views/branchdefination/index.jsx'))
const AdressDefination = React.lazy(() => import('./views/adressdefination/index.jsx'))
const FormDefinationType = React.lazy(() => import('./views/formdefinations/index.jsx'))
const ProductDefination = React.lazy(() => import('./views/productdefination/index.jsx')) 
const UserDefination = React.lazy(() => import('./views/userdefination/index.jsx'))
const FormDefinationEdit=  React.lazy(() => import('./views/formdefinationEdit/index.jsx'))
const DigitalForms=React.lazy(() => import('./views/digitalForms/index.jsx'))
const DigitalFormEdit=React.lazy(() => import('./views/digitalFormEdit/index.jsx'))
const FormDefinationAutoComplate=React.lazy(() => import('./views/formdefinationsAComp/index.jsx'))
const FormDefinationVersion=React.lazy(()=>import('./views/formdefinationversion/index.jsx') );
const FormDefinationAttachments=React.lazy(()=>import('./views/definationattachments/index.jsx') );
const WorkFlowDefination = React.lazy(()=>import('./views/workflowDefination/index.jsx'));
const workflow = React.lazy(()=>import('./views/workflow/index.jsx'));
const Settings = React.lazy(()=>import('./views/settings/index.jsx'));
const UserProfile = React.lazy(()=>import('./views/Profile/index.jsx'));
const CustomeFields=React.lazy(()=>import('./views/customefields/index.jsx'));
const customeFieldEdit=React.lazy(()=>import('./views/customefieldEdit/index.jsx'));
const CustomWorks=React.lazy(()=>import('./views/customworks/index.jsx'));


const routes = [

  
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', element: Dashboard }, 
  { path: '/BranchDefination', name: 'BranchDefination', element: BranchDefination },   
  { path: '/AdressDefination', name: 'AdressDefination', element: AdressDefination }, 
  { path: '/FormDefinationType', name: 'FormDefinationType', element: FormDefinationType }, 
  { path: '/productdefination', name: 'productdefination', element: ProductDefination }, 
  { path: '/userdefination', name: 'Userdefination', element: UserDefination }, 
  { path: '/FormDefinationTypeEdit', name: 'FormDefinationType/FormDefinationEdit', element: FormDefinationEdit }, 
  
  { path: '/digitalForms', name: 'DigitalForms', element: DigitalForms }, 
  { path: '/digitalFormEdit', name: 'DigitalForms/digitalFormEdit', element: DigitalFormEdit }, 
  { path: '/formdefinationsAComp', name: 'FormDefinationType/FormDefinationEdit/AutoComplate', element: FormDefinationAutoComplate }, 
  { path: '/FormDefinationVersion', name: 'FormDefinationType/FormDefinationVersion', element: FormDefinationVersion }, 
  { path: '/definationattachments', name: 'FormDefinationType/FormDefinationEdit/definationattachments', element: FormDefinationAttachments }, 

  { path: '/workflowDefination', name: 'workflowDefination', element: WorkFlowDefination }, 
  { path: '/workflow', name: 'workflow', element: workflow }, 
  { path: '/Settings', name: 'Settings', element: Settings }, 
  { path: '/UserProfile', name: 'UserProfile', element: UserProfile }, 
  { path: '/customeFields', name: 'CustomeFields', element: CustomeFields }, 
  { path: '/CustomeFieldEdit', name: 'CustomeFieldEdit', element: customeFieldEdit }, 
  { path: '/CustomWorks', name: 'CustomWorks', element: CustomWorks }, 

  

  
  
  
  
  
]
export default routes


