using Cloud.Framework.Strategy;

namespace Cloud.Framework.Assembly
{
    public interface IAssemblyKeyStrategy : IStrategy
    {
        /// <summary>
        /// 手机号验证码Key生成策略
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        string PhoneVerificationCode(string phoneNumber);

        string AssemblyJsKey();
    }
}