using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Diagnostics;
//using Microsoft.Practices.ServiceLocation;
using XuatThuy.Controls;

namespace XuatThuy.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ViewModelMain>();
            SimpleIoc.Default.Register<PlcViewModel>();
            SimpleIoc.Default.Register<SoLoVM>();
            SimpleIoc.Default.Register<PhieuChatLuongVM>();

            SimpleIoc.Default.Register<CaiDatVM>();
            SimpleIoc.Default.Register<TongQuanVM>();
            SimpleIoc.Default.Register<ViewModelPhieuCan>();
            SimpleIoc.Default.Register<LoginVM>();
            SimpleIoc.Default.Register<DSPhieuCanVM>();
            SimpleIoc.Default.Register<ThongKeVM>();
            SimpleIoc.Default.Register<BienBanGiaoNhanVM>();
        }

        public ViewModelMain ViewModelMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModelMain>();
            }
        }

        public LoginVM LoginVM => ServiceLocator.Current.GetInstance<LoginVM>();
        public PlcViewModel PlcViewModel => ServiceLocator.Current.GetInstance<PlcViewModel>();
        public ViewModelPhieuCan ViewModelPhieuCan => ServiceLocator.Current.GetInstance<ViewModelPhieuCan>();
        public TongQuanVM TongQuanVM => ServiceLocator.Current.GetInstance<TongQuanVM>();
        public CaiDatVM CaiDatVM => ServiceLocator.Current.GetInstance<CaiDatVM>();
        public SoLoVM SoLoVM => ServiceLocator.Current.GetInstance<SoLoVM>();
        public PhieuChatLuongVM PhieuChatLuongVM => ServiceLocator.Current.GetInstance<PhieuChatLuongVM>();
        public DSPhieuCanVM DSPhieuCanVM => ServiceLocator.Current.GetInstance<DSPhieuCanVM>();
        public ThongKeVM ThongKeVM => ServiceLocator.Current.GetInstance<ThongKeVM>();
        public BienBanGiaoNhanVM BienBanGiaoNhanVM => ServiceLocator.Current.GetInstance<BienBanGiaoNhanVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}