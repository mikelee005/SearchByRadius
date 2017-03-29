using adr.Web.Domain;
using adr.Web.Enums;
using adr.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adr.Web.Services.Interfaces
{
    public interface IAddressService
    {
        List<AddressDomain> GetAllAddresses();

        AddressDomain GetAddressById(AddressRequest model);

        List<AddressDomain> GetAddressesByCompanyId(int companyId);

        List<AddressDomain> GetAddressesByCompanyAndType(int companyId, AddressType type);

        List<AddressDomain> GetByCompanyIdAndAddressType(AddressRequest model);

        int InsertAddress(AddressRequiredRequest model);

        bool UpdateAddress(AddressUpdateRequest model);

        bool DeleteAddress(int id);

        AddressBookDomain GetAddressBookByCompanyId(int id);

        LatLngDomain AddressGetLatLng(string address);

        List<AddressDistanceDomain> GetAddressesInRadius(LatLngRadiusRequest model);

    }
}