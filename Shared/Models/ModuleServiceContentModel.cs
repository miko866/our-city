using Shared.Models.ModuleSimplePage;
using Shared.Models.ModuleSpecialAnnouncement;

namespace Shared.Models;

public record ModuleServiceContentModel : ModuleServiceModel
{
    public IEnumerable<ModuleSpecialAnnouncementMobileModel>? ModuleSpecialAnnouncements { get; set; }
    public IEnumerable<ModuleSimplePageMobileModel>? ModuleSimplePages { get; set; }
}
