using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using encryption;
using Crud;
using EntityModel;

namespace Service
{
    public class SecurityService
    {

        MD5encryptor md = new MD5encryptor();

        UserCrubOperator userCrudOp = new UserCrubOperator();

        public Boolean authentication(string userid, string pwd) {
            user user = userCrudOp.queryById(userid);
            return md.match(user.password, pwd, userid);
        }

    }
}
