 
   
import  i18   from  '../../translation/i18'
 
 
  
export  const Gridcolumns = [
  {
    accessorKey: 'id', //access nested data with dot notation
    header: 'Id',
    size: 50,
  },
  {
    accessorKey: 'userName',
    header:  i18.t('UserName'),
    size: 150,
  },
  {
    accessorKey: 'fullName',
    header:  i18.t('FullName'),
    size: 150,
  },
  {
    accessorKey: 'phoneNumber',
    header:  i18.t('PhoneNumber'),
    size: 150,
  },
  {
    accessorKey: 'email',
    header:  i18.t('Email'),
    size: 150,
  },
  {
    accessorKey: 'branchName',
    header:  i18.t('BranchName'),
    size: 150,
  },
]; 

export default {
  Gridcolumns,
}