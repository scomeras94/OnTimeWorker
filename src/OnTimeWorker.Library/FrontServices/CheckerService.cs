using OnTimeWorker.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.FrontServices
{
    public class CheckerService
    {
        public Error CheckId(int id)
        {
            if (id <= 0)
                return new Error { code = 1, program = "CheckerService", text = "CheckId()", message = "Id incorrecto" };

            return null;
        }
        public Error CheckIdentityDocument(string identityDocument)
        {
            if (identityDocument == null || identityDocument == "")
            {
                return new Error { code = 2, program = "CheckerService", text = "CheckIdentityDocument()", message = "Documento de identidad del trabajador no informado" };
            }

            if (identityDocument.Length != 9)
            {
                return new Error { code = 3, program = "CheckerService", text = "CheckIdentityDocument()", message = "Documento de identidad longitud inválida" };
            }

            string numeros = identityDocument.Substring(0, 8);
            char letra = identityDocument.Substring(identityDocument.Length - 1)[0];

            if (!int.TryParse(numeros, out _))
            {
                return new Error { code = 4, program = "CheckerService", text = "CheckIdentityDocument()", message = "Documento de identidad inválido" };
            }

            return null;
        }
        public Error CheckName(string name)
        {
            if (name == null || name == "")
            {
                return new Error { code = 5, program = "CheckerService", text = "CheckName()", message = "Nombre del empleado no informado" };
            }

            if (name.Length > 250)
            {
                return new Error { code = 6, program = "CheckerService", text = "CheckName()", message = "Nombre del empleado longitud inválida" };
            }

            return null;
        }
        public Error CheckSecondName(string secondName)
        {
            if (secondName == null || secondName == "")
            {
                return new Error { code = 7, program = "CheckerService", text = "CheckSecondName()", message = "Apellidos del empleado no informados" };
            }

            if (secondName.Length > 500)
            {
                return new Error { code = 8, program = "CheckerService", text = "CheckSecondName()", message = "Apellidos del empleado longitud inválida" };
            }

            return null;
        }
        public Error CheckPhone(int phone)
        {
            if (phone.ToString().Length != 9)
            {
                return new Error { code = 9, program = "CheckerService", text = "CheckPhone()", message = "Teléfono del empleado no informado" };
            }

            if (!int.TryParse(phone.ToString(), out _))
            {
                return new Error { code = 10, program = "CheckerService", text = "CheckPhone()", message = "Teléfono del empleado no numérico" };
            }

            char primerDigito = phone.ToString()[0];
            if (primerDigito != '6' && primerDigito != '7' && primerDigito != '8' && primerDigito != '9')
            {
                return new Error { code = 11, program = "CheckerService", text = "CheckPhone()", message = "Teléfono del empleado formato incorrecto" };
            }

            return null;
        }
        public Error CheckEmail(string email)
        {
            if (email == null || email == "")
            {
                return new Error { code = 12, program = "CheckerService", text = "CheckEmail()", message = "Email del empleado no informado" };
            }

            if (email.Length > 250)
            {
                return new Error { code = 13, program = "CheckerService", text = "CheckEmail()", message = "Email del empleado longitud inválida" };
            }

            if (!email.Contains("@"))
            {
                return new Error { code = 14, program = "CheckerService", text = "CheckEmail()", message = "Email del empleado formato incorrecto (@)" };
            }

            var splitMail = email.Split("@");
            if (splitMail == null || splitMail.Length != 2)
            {
                return new Error { code = 15, program = "CheckerService", text = "CheckEmail()", message = "Email del empleado formato inválido (xx@xx)" };
            }

            var splitMail2 = splitMail[1].Split(".");
            if (splitMail2 == null || splitMail2.Length != 2)
            {
                return new Error { code = 16, program = "CheckerService", text = "CheckEmail()", message = "Email del empleado formato inválido (xx@xx.xxx)" };
            }

            return null;
        }
        public Error CheckPwd(string pwd)
        {
            if (pwd == null || pwd == "")
            {
                return new Error { code = 17, program = "CheckerService", text = "CheckPwd()", message = "Contraseña del usuario no informada" };
            }

            if (pwd.Length > 500)
            {
                return new Error { code = 18, program = "CheckerService", text = "CheckPwd()", message = "Contraseña del usuario longitud inválida" };
            }

            return null;
        }
        public Error CheckDate(string date)
        {
            if (date == null || date == "")
            {
                return new Error { code = 19, program = "CheckerService", text = "CheckDate()", message = "Fecha del fichaje no informada" };
            }

            return null;
        }
        public Error CheckTime(string time)
        {
            if (time == null || time == "")
            {
                return new Error { code = 20, program = "CheckerService", text = "CheckTime()", message = "Tiempo de inicio no informado" };
            }

            if (time.Length != 8)
            {
                return new Error { code = 3, program = "CheckerService", text = "CheckTime()", message = "Tiempo de inicio longitud inválida" };
            }

            var startSplit = time.Split(":");
            if (startSplit == null || startSplit.Length != 3)
            {
                return new Error { code = 4, program = "CheckerService", text = "CheckTime()", message = "Tiempo de inicio formato inválido" };
            }

            var isNumeric0 = int.TryParse(startSplit[0], out _);
            var isNumeric1 = int.TryParse(startSplit[1], out _);
            var isNumeric2 = int.TryParse(startSplit[2], out _);
            if (!isNumeric0 || !isNumeric1 || !isNumeric2)
            {
                return new Error { code = 5, program = "CheckerService", text = "CheckTime()", message = "Tiempo de inicio partes no numéricas" };
            }

            return null;
        }
        public List<Error> DiscardNullErrors(List<Error> allErrors)
        {
            List<Error> filteredErrors = new List<Error>();
            foreach (Error i in allErrors)
            {
                if (i != null)
                    filteredErrors.Add(i);
            }

            if (filteredErrors.Count > 0)
                return filteredErrors;

            return null;
        }
    }
}
