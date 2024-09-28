import React from 'react'
import {
  CAvatar,
  CBadge,
  CDropdown,
  CDropdownDivider,
  CDropdownHeader,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle,
} from '@coreui/react'
import {
  cilBell,
  cilCreditCard,
  cilCommentSquare,
  cilEnvelopeOpen,
  cilFile,
  cilLockLocked,
  cilSettings,
  cilTask,
  cilUser,
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'

import avatar8 from './../../assets/images/avatars/8.jpg'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom'
import { useTranslation } from "react-i18next";

import { Link } from 'react-router-dom';
const AppHeaderDropdown = () => {


  const { t } = useTranslation();
  const navigate = useNavigate();


  const dispatch = useDispatch();

  async function LogOut() {
    dispatch({ type: 'set', userToken: null });
    localStorage.removeItem("LastToken");
    navigate('../Login');

  }


  return (
    <CDropdown variant="nav-item">
      <CDropdownToggle placement="bottom-end" className="py-0" caret={false}>
        <CAvatar src={avatar8} size="md" />
      </CDropdownToggle>
      <CDropdownMenu className="pt-0" placement="bottom-end">


        <CDropdownHeader className="bg-light fw-semibold py-2">Settings</CDropdownHeader>
        <CDropdownItem href="#">
        <Link to={{
            pathname: '/UserProfile',
          }} >
          <CIcon icon={cilUser} className="me-2" />
          Profile
          </Link>
        </CDropdownItem>
        <CDropdownItem href="#">

          <Link to={{
            pathname: '/Settings',
          }} >
            <CIcon icon={cilSettings} className="me-2" />
            Settings
          </Link>
        </CDropdownItem>

        <CDropdownDivider />
        <CDropdownItem href="#">
          <CIcon icon={cilLockLocked} className="me-2" onClick={LogOut} />
          Lock Out
        </CDropdownItem>
      </CDropdownMenu>
    </CDropdown>
  )
}

export default AppHeaderDropdown
