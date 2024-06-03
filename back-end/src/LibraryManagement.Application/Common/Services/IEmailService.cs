using LibraryManagement.Application.Models.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Common.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}