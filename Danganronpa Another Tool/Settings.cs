namespace Danganronpa_Another_Tool.Properties {
    
    
    // La classe consente la gestione di eventi specifici sulla classe delle impostazioni:
    //  L'evento SettingChanging viene generato prima della modifica di un valore dell'impostazione.
    //  L'evento PropertyChanged viene generato dopo la modifica di un valore dell'impostazione.
    //  L'evento SettingsLoaded viene generato dopo il caricamento dei valori dell'impostazione.
    //  L'evento SettingsSaving viene generato prima del salvataggio dei valori dell'impostazione.
    internal sealed partial class Settings {
        
        public Settings() {
            // // Per aggiungere gestori eventi per il salvataggio e la modifica delle impostazioni, annullare l'impostazione come commento delle righe seguenti:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Aggiungere qui il codice per gestire l'evento SettingChangingEvent.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Aggiungere qui il codice per gestire l'evento SettingsSaving.
        }
    }
}
